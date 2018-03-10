
$("#categoria").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));  
})

$("#ANSI").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})

$("#competencia").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})

$("#rol").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})


$("#tipoPuesto").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})
