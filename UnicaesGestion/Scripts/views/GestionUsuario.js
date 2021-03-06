﻿

$(document).ready(init);

function init() {
    $("#date").datepicker();
    init_stepy();
}

function init_stepy() {
    $('#wfrmusuario').stepy({
        backLabel: 'Atras',
        block: true,
        nextLabel: 'Siguiente',
        titleClick: true,
        titleTarget: "#stepy-tab",
        legend: false
    });
}


function loading(obj) {
    $(obj).html("<div style='text-align:right;'><br/><img src='/Content/img/loading.gif' alt='cargando..' width='45' height='45' /> procesando..</div>");
}

function showError(obj, msj) {
    var html = "<div class='alert alert- block alert-danger fade in'>";
    html += "<button data-dismiss='alert' class='close close-sm' type= 'button' >";
    html += "<i class='fa fa-times'></i></button> <strong>Error</strong>";
    html += msj;
    html += "</div >";
    $(obj).html(html);
}

//paso 1
function ProcesarPaso1() {
    $("#wfrmusuario").validate({
        rules: {
            nombre: {
                required: true
            },
            apellido: {
                required: true
            },
            cmbPuestoTrabajo: {
                min: 1
            }          
        },
        messages: {
            nombre: {
                required: "Indique sus nombres"
            },
            apellido: {
                required: "Indique sus apellidos"
            },
            cmbPuestoTrabajo: {
                min: "Debe seleccionar un puesto de trabajo"
            }            
        },
        submitHandler: function (form) {
            //cuando el formulario es válido.
            Next1();          

        },
        invalidHandler: function (event, validator) {
            //cuando el formulario tiene errores. 
        },
        highlight: function (element, errorClass) {
            $(element).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (element, errorClass) {
            $(element).closest(".form-group").removeClass("has-error");
        }
    });


    var d = $("#wfrmusuario").submit();

}

function GuardarPaso1() {
    var id = $("#usuarioid").val();
    var nombre = $("#nombre").val();
    var apellido = $("#apellido").val();
    var puesto = $("#cmbPuestoTrabajo").val();   
    var action;
    var data;

    if (typeof id === "undefined" || id === "0") {
        action = "/Personals/CrearUsuario/";
        data = { "nombre": nombre, "apellido": apellido, "puesto": puesto };
    } else {
        action = "/PerfilTrabajo/ModificarUsuario/";
        data = { "id": id, "nombre": nombre, "apellido": apellido, "puesto": puesto };
    }

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {

            $("#usuarioid").val(response.data);
            $("#btnsiguiente1").show();
            $("#loading1").html("");
            Next1();
        }, error: function (response) {
            $("#btnsiguiente1").show();
            showError($("#loading1"), response.data);
        }
    });

}

function Next1() {
    $("#default-title-1").addClass("current-step");
    $("#default-title-0").removeClass("current-step");
    $('#wfrmusuario').stepy("step", 1);
    }

//paso 2
function obteniendoFunciones() {

    loading($("#funciones_puesto"), "Cargando funciones de puesto de trabajo");
    var id = $("#puestoid").val();
    $.ajax({
        url: "/PerfilTrabajo/FuncionesPuestoTrabajo",
        method: "POST",
        data: { id: id },
        success: function (response) {
            $("#funciones_puesto").html(response);
        },
        error: function (response) {
            showError($("#funciones_puesto"), "No se pudieron cargar las funciones del puesto de trabajo");
        }
    });

}

function dlgaddfuncion() {
    $("#txtfuncion").val("");
    $("#mdaddfuncion").modal();
    $("#idfuncion").val("0");
}


function GuardarFuncion() {
    $("#frmfunciones").validate({
        rules: {
            txtfuncion: {
                required: true
            }
        },
        messages: {
            txtfuncion: "Debe indicar la función.."
        },
        submitHandler: function (form) {
            ajaxGuardarFuncion();

        }, highlight: function (element, errorClass) {
            $(element).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (element, errorClass) {
            $(element).closest(".form-group").removeClass("has-error");
        }
    });
    $("#frmfunciones").submit();


}

function ajaxGuardarFuncion() {
    var idfuncion = $("#idfuncion").val();
    var idpuesto = $("#puestoid").val();
    var funcion = $("#txtfuncion").val();
    var action = "";
    var data;

    if (idfuncion === "0") {
        action = "/PerfilTrabajo/AgregarFuncion";
        data = { "idpuesto": idpuesto, "funcion": funcion };

    } else {
        action = "/PerfilTrabajo/EditarFuncion";
        data = { "idfuncion": idfuncion, "funcion": funcion };

    }

    $("#mdaddfuncion").modal("hide");
    loading($("#loading2"), "Procesando");

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {
            if (response.result === "success") {
                $("#loading2").html("");
                obteniendoFunciones();

            } else {
                showError($("#loading2"), response.data);
            }
            $("#idfuncion").val("0");
        },
        error: function (response) {
            showError($("#loading2"), response.data);
            $("#idfuncion").val("0");
        }
    });

}

function EditarFuncion(id) {
    $("#idfuncion").val(id);
    var funcion = $("#funcion_" + id).html();
    $("#txtfuncion").val(funcion.trim());
    $("#mdaddfuncion").modal();
}

function Back2() {
    $("#default-title-0").addClass("current-step");
    $("#default-title-1").removeClass("current-step");
    $('#wfrmusuario').stepy("step", 0);
}

function dlgeliminar(id) {
    $("#idfunciondel").val(id);
    $("#mddelfuncion").modal();
}

function confirmarEliminar() {
    $("#mddelfuncion").modal("hide");
    loading($("loading2"), "Eliminando función..");
    var idfuncion = $("#idfunciondel").val();
    $.ajax({
        url: "/PerfilTrabajo/EliminarFuncion",
        method: "POST",
        data: { "id": idfuncion },
        success: function (response) {
            if (response.result === "success") {
                obteniendoFunciones();
                $("loading2").html("");
            } else {
                showError($("loading2"), response.data);
            }

        }, error: function (response) {
            showError($("loading2"), "Error procesando transacción...");
        }
    });

}

function Next2() {
    $("#default-title-2").addClass("current-step");
    $("#default-title-1").removeClass("current-step");
    $('#wfrmusuario').stepy("step", 2);
}

//paso 3


