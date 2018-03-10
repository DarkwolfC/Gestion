

$(document).ready(init);

function init() {
    $("#date").datepicker();
    init_stepy();
}

function init_stepy() {
    $('#wfrmpuesto').stepy({
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
    $("#wfrmpuesto").validate({
        rules: {
            nombre: {
                required: true
            },
            objetivo: {
                required: true
            },
            cmbunidad: {
                min: 1
            }           
        },
        messages: {
            nombre: {
                required: "Indique el nombre de la unidad"
            },
            objetivo: {
                required: "Debe indicar el objetivo de la unidad"
            },
            cmbunidad: {
                min: "Debe seleccionar la unidad de la que depende"
            }           
        },
        submitHandler: function (form) {
            //cuando el formulario es válido.
            loading($("#loading1"));
            $("#btnsiguiente1").hide();
            GuardarPaso1();

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


    var d = $("#wfrmpuesto").submit();

}

function GuardarPaso1() {
    var id = $("#unidadid").val();
    var nombre = $("#nombre").val();
    var objetivo = $("#objetivo").val();   
    var depende = $("#cmbunidad").val();    
    var action;
    var data;

    if (typeof id == "undefined" || id === "0") {
        action = "/Unidades/CrearUnidad/";
        data = { "nombre": nombre, "objetivo": objetivo, "depende": depende }
    } else {
        action = "/Unidades/ModificarUnidad/";
        data = { "id": id, "nombre": nombre, "objetivo": objetivo, "depende": depende  }
    }

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {

            $("#unidadid").val(response.data);
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
    $('#wfrmpuesto').stepy("step", 1);
    obteniendoFunciones();
}

//paso 2
function obteniendoFunciones() {

    loading($("#funciones_unidad"), "Cargando funciones de puesto de la unidad");
    var id = $("#unidadid").val();
    $.ajax({
        url: "/Unidades/FuncionesUnidad",
        method: "POST",
        data: { id: id },
        success: function (response) {
            $("#funciones_unidad").html(response);
            toastr.success('Unidad insertada correctamente'); //toast
        },
        error: function (response) {
            showError($("#funciones_unidad"), "No se pudieron cargar las funciones de la unidad");
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
    var idunidad = $("#unidadid").val();
    var funcion = $("#txtfuncion").val();
    var action = "";
    var data;

    if (idfuncion === "0") {
        action = "/Unidades/AgregarFuncion";
        data = { "idunidad": idunidad, "funcion": funcion };

    } else {
        action = "/Unidades/EditarFuncion";
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
                toastr.success('Función insertada correctamente'); //toast
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
    $('#wfrmpuesto').stepy("step", 0);
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
        url: "/Unidades/EliminarFuncion",
        method: "POST",
        data: { "id": idfuncion },
        success: function (response) {
            if (response.result === "success") {
                obteniendoFunciones();  
                toastr.success('Función Eliminada correctamente'); //toast
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
   
}

// validacion para Caracteres numericos  
$("#nombre").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})

  
