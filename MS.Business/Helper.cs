using System.Linq;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MS.Business
{
    public enum Operation
    {
        Create,
        Edit,
        Delete
    }

    public static class Helper
    {
        #region "Public Methods"

        #region "Save & Delete Operations"

        /// <summary>
        /// Save the reocrd to related table, if the primary key passed update it else add it. V 1.1
        /// </summary>
        /// <param name="objEntity">Entity Object</param>
        public static void Save(this EntityObject objEntity)
        {
            objEntity.AttachMe();

            try                                                         // Update Record
            {
                Global.Context.ObjectStateManager.ChangeObjectState(objEntity, EntityState.Modified);
                Global.Context.SaveChanges();
            }
            catch (OptimisticConcurrencyException)                      // Insert Record
            {
                Global.Context.ObjectStateManager.ChangeObjectState(objEntity, EntityState.Added);
                Global.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete the record from related table. The primary key value must be filled.
        /// </summary>
        /// <param name="objEntity">Entity Object</param>
        public static void Delete(this EntityObject objEntity)
        {
            if (objEntity != null)
            {
                objEntity.AttachMe();
                Global.Context.ObjectStateManager.ChangeObjectState(objEntity, EntityState.Deleted);
                Global.Context.SaveChanges();
            }
        }

        #endregion

        #region "Other Operations"

        /// <summary>
        /// Attach object with current context
        /// </summary>
        /// <param name="objEntity">Entity Object</param>
        public static void AttachMe(this EntityObject objEntity)
        {
            Global.Context.AttachObject(objEntity);
        }

        /// <summary>
        /// Attach an object with Operation (Create, Edit and Delete)
        /// </summary>
        /// <param name="objEntity"></param>
        /// <param name="oper"></param>
        public static void AttachMe(this EntityObject objEntity, Operation oper)
        {
            Global.Context.AttachObject(objEntity, oper);
        }

        /// <summary>
        /// Attach a collection with operation (Create, Edit and Delete)
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="oper"></param>
        public static void AttachUs<T>(this EntityCollection<T> entities, Operation oper) where T : EntityObject
        {
            entities.ForEach(entity => Global.Context.AttachObject(entity, oper));
        }

        /// <summary>
        /// Detach object with current context
        /// </summary>
        /// <param name="entity">Entity Object</param>
        public static void DetachMe(this EntityObject entity)
        {
            Global.Context.DetachObject(entity);
        }

        /// <summary>
        /// Detach a collection from context
        /// </summary>
        /// <param name="entities"></param>
        public static void DetachUs<T>(this EntityCollection<T> entities) where T : EntityObject
        {
            entities.ForEach(entity => Global.Context.DetachObject(entity));
        }

        /// <summary>
        /// Get entity set from object context
        /// </summary>
        /// <param name="Context">Object Context</param>
        /// <param name="entity">Entity Object</param>
        /// <returns></returns>
        public static string GetEntitySetName(this ObjectContext Context, EntityObject entity)
        {
            string entityTypeName = entity.GetType().Name;
            var container = Context.MetadataWorkspace.GetEntityContainer(Context.DefaultContainerName, DataSpace.CSpace);
            return container.BaseEntitySets.FirstOrDefault(meta => meta.ElementType.Name == entityTypeName).Name;
        }

        /// <summary>
        /// Attach object with context
        /// </summary>
        /// <param name="context">Object Context</param>
        /// <param name="objEntity">Entity Object</param>
        public static void AttachObject(this ObjectContext context, EntityObject objEntity)
        {
            if (context.IsObjectAttached(objEntity) == false)
            {
                context.AttachTo(context.GetEntitySetName(objEntity), objEntity);
            }
        }

        /// <summary>
        /// Attach and obejct with operation (Create, Edit and Delete)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="objEntity"></param>
        /// <param name="oper"></param>
        public static void AttachObject(this ObjectContext context, EntityObject objEntity, Operation oper)
        {
            if (context.IsObjectAttached(objEntity) == false)
                context.AttachObject(objEntity);

            context.ObjectStateManager.ChangeObjectState(objEntity, GetEntityState(oper));
        }

        /// <summary>
        /// Detach object with context
        /// </summary>
        /// <param name="Context">Object Context</param>
        /// <param name="entity">Entity Object</param>
        public static void DetachObject(this ObjectContext Context, EntityObject entity)
        {
            if (entity.EntityKey != null)
            {
                object objentity = null;
                var exist = Context.TryGetObjectByKey(entity.EntityKey, out objentity);
                if (exist) { Context.Detach(entity); }
            }
        }

        #endregion

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Check weather a object is attached with context or not
        /// </summary>
        /// <param name="Context">Object Context</param>
        /// <param name="entity">Entity Object</param>
        /// <returns></returns>
        private static bool IsObjectAttached(this ObjectContext Context, EntityObject entity)
        {
            if (entity.EntityKey != null)
            {
                object objentity = null;
                var exist = Context.TryGetObjectByKey(entity.EntityKey, out objentity);
                return exist;
            }

            return false;
        }

        /// <summary>
        /// Get all navigation properties from current entity
        /// </summary>
        /// <param name="entity">Entity Object</param>
        /// <returns></returns>
        private static List<PropertyInfo> GetNavigationProperties(this EntityObject entity)
        {
            return entity.GetType().GetProperties().Where(pt => pt.PropertyType.IsSubclassOf(typeof(EntityObject)) || (pt.PropertyType.IsGenericType
                    && pt.PropertyType.GetGenericTypeDefinition().UnderlyingSystemType == typeof(EntityCollection<>))).ToList();

        }

        /// <summary>
        /// Get Entity State
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        private static EntityState GetEntityState(Operation oper)
        {
            switch (oper)
            {
                case Operation.Create:
                    return EntityState.Added;
                case Operation.Edit:
                    return EntityState.Modified;
                case Operation.Delete:
                    return EntityState.Deleted;
            }

            throw new ArgumentOutOfRangeException();
        }

        #endregion
    }
}
