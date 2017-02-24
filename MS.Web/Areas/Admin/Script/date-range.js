
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-range-pre": function (a) {
        return Date.parse(a);
    },
    "date-range-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "date-range-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});