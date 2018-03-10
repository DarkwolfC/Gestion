

$(document).ready(init);

$("#titulo").keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) {
        toastr.info('Porfavor no utilice caracteres numericos');
        return '';
    }));
})

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
            titulo: {
                required:true
            },
            objetivo: {
                required:true
            }, 
            cmbunidad: {
                min:1
            },
            cmbtipopuesto: {
                min: 1
            },
            fechaaprobacion: {
                required: true                               
            }
        },
        messages: {
            titulo: {
                required: "Indique el nombre del puesto de trabajo"
            }, 
            objetivo: {
                required: "Debe indicar el objetivo del puesto de trabajo"
            },
            cmbunidad: {
                min: "Debe seleccionar la unidad a la que pertenece el puesto de trabajo"
            },
            cmbtipopuesto: {
                min: "Debe indicar el tipo de puesto de trabajo"
            },
            fechaaprobacion: {
                required: "Debe indicar la fecha de aprobación"
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
    var id = $("#puestoid").val();
    var titulo = $("#titulo").val();
    var objetivo = $("#objetivo").val();
    var jefe = $("#cmbjefeinmediato").val();
    var unidad = $("#cmbunidad").val();
    var tipo = $("#cmbtipopuesto").val();
    var fecha = $("#fechaaprobacion").val();
    var estado = $("input[name=estado]:checked").val();
    var aprobacion = $("input[name=aprobacion]:checked").val();
    var action;
    var data;
    
    if (typeof id == "undefined" ||  id==="0") {
        action = "/PerfilTrabajo/CrearPuestoTrabajo/";
        data = { "titulo": titulo, "objetivo": objetivo, "jefe": jefe , "unidad": unidad, "tipo": tipo, "fecha":fecha, "estado":estado, "aprobacion":aprobacion }
    } else {
        action = "/PerfilTrabajo/ModificarPuestoTrabajo/";
        data = { "id":id, "titulo": titulo, "objetivo": objetivo, "jefe": jefe, "unidad": unidad, "tipo": tipo, "fecha": fecha, "estado": estado, "aprobacion": aprobacion }
    }

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {
           
            $("#puestoid").val(response.data);
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
    
    loading($("#funciones_puesto"), "Cargando funciones de puesto de trabajo");
    var id = $("#puestoid").val();
    $.ajax({
        url: "/PerfilTrabajo/FuncionesPuestoTrabajo",
        method: "POST",
        data: { id: id }, 
        success: function(response) {
            $("#funciones_puesto").html(response);
        },
        error: function(response) {
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

        }, error: function(response) {
            showError($("loading2"), "Error procesando transacción...");
        }
    });

}

function Next2() {
    $("#default-title-2").addClass("current-step");
    $("#default-title-1").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 2);
    cargarRequisitos();
}

//paso 3
function cargarRequisitos() {
    var idpuesto = $("#puestoid").val();
    var div = $("#contentRequisitos");
    loading(div, "Cargando requisitos de puesto");
    $.ajax({
        url: "/PerfilTrabajo/RequisitosPuestoTrabajo/",
        data: { "idpuesto": idpuesto },
        method: "POST",
        success: function (response) {
            $(div).html(response);
        },
        error: function (error) {
            showError($(div), "Error cargando categorías");
        }
    });

}


function dlgaddRequisito() {
    $("#idrequisito").val("0");
    $("#cmbcategoria").val("0");
    $("#txtrequisito").val("")
    $("#mdaddRequisitos").modal();

}

function guardarRequisito() {
    $("#frmrequisitos").validate({
        rules: {
            cmbcategoria: {
                min:1
            },
            txtrequisito: {
                required: true
            }
        },
        messages: {
            cmbcategoria: "Debe indicar la categoría..",
            txtrequisito: "Debe indicar el requisito.."
        },
        submitHandler: function (form) {
            ajaxGuardarRequisito();

        }, highlight: function (element, errorClass) {
            $(element).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (element, errorClass) {
            $(element).closest(".form-group").removeClass("has-error");
        }
    });
    $("#frmrequisitos").submit();
}

function ajaxGuardarRequisito() {
    var idcategoria = $("#cmbcategoria").val();
    var requisito = $("#txtrequisito").val();
    var idrequisito = $("#idrequisito").val();
    var idpuesto = $("#puestoid").val();
    var div = $("#loadingCategoria").val();
    loading(div, "procesando..");

    $.ajax({
        url: "/PerfilTrabajo/AgregarRequisito",
        data: { "idpuesto": idpuesto, "idcategoria": idcategoria, "descripcion": requisito },
        method: "POST",
        success: function (response) {
            if (response.result == "success") {
                $(div).html("");
                $("#mdaddRequisitos").modal("hide");
                cargarRequisitos();
            } else {
                showError($(div), response.msj);
            }
        },
        error: function (response) {
            showError($(div), "Error intentando guardar categoria");
        }
    })

}

function Next3() {
    $("#default-title-3").addClass("current-step");
    $("#default-title-2").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 3);
    
}

function Back3() {
    $("#default-title-1").addClass("current-step");
    $("#default-title-2").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 1);
}

function Back4() {
    $("#default-title-2").addClass("current-step");
    $("#default-title-3").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 2);
}