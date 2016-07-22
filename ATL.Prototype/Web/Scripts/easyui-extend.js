$.extend($.fn.validatebox.defaults.rules, {
    maxLength: {
        validator: function (value, param) {
            return value.length <= param[0];
        },
        message: '少于{0}个字符.'
    },
    alpha: {
        validator: function (value, param) {
            return (/^[A-Za-z]+$/.test(value));
        },
        message: '请输入字母.'
    },
    number: {
        validator: function (value) {
            var reg = /^\d+(\.\d+)?$/i
            return reg.test(value);
        },
        message: '此项必须为数字'
    }

});