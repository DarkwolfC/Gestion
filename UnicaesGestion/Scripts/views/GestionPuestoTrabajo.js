

$(document).ready(init);

function init() {    
    init_stepy();
}

function init_stepy() {
        $('#wfrmpuesto').stepy({
            backLabel: 'Atras',
            block: true,
            nextLabel: 'Siguiente',
            titleClick: true,
            titleTarget: "#stepy-tab",
            legend: false,
            next: function (index, total) {
                var dt = index;
                var element = "#default-title-" + dt;
                $("#default-title-" + dt).addClass("current-step");
                dt = dt - 1;
                $("#default-title-" + dt).removeClass("current-step");

                switch (index) {
                    case 1:
                        EnviarPaso1();
                }
            },
            back: function (index, total) {
                var dt = index;
                var element = "#default-title-" + dt;
                $("#default-title-" + dt).addClass("current-step");
                dt = dt + 1;
                $("#default-title-" + dt).removeClass("current-step");
            },            
            validate: function () {
                return $("#")
            }
        });   
}

function validacion_paso1() {
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
            alert("process");
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

}


function EnviarPaso1() {
    validacion_paso1();
    var d = $("#wfrmpuesto").submit();
    alert(d);

    //invalido = false;
    //var titulo = $("#titulo").val();
    //if (titulo.length === 0) {
    //    validator.showErrors({
    //        'titulo': "<span style='color:red;'>Campo requerido</span>"
    //    });
    //    invalido = true;
    //}
    //var objetivo = $("#objetivo").val();
    //if (objetivo.length === 0) {
    //    validator.showErrors({
    //        'objetivo': "<span style='color:red;'>Campo requerido</span>"
    //    });
    //    invalido = true;
    //}

    //var jefe = $("#cmbjefeinmediato").val();

    //var unidad = $("#cmbunidad").val();

    //if (unidad === "0") {
    //    validator.showErrors({
    //        'cmbunidad': "<span style='color:red;'>Campo requerido</span>"
    //    });
    //    invalido = true;
    //}

    //var tipo = $("#cmbtipopuesto").val();
    //if (tipo === 0) {
    //    validator.showErrors({
    //        'cmbtipopuesto': "<span style='color:red;'>Campo requerido</span>"
    //    });
    //    invalido = true;
    //}


    //var fecha = $("#fechaaprobacion").val();
    //if (fecha === 0) {
    //    validator.showErrors({
    //        'fechaaprobacion': "<span style='color:red;'>Campo requerido</span>"
    //    });
    //    invalido = true;
    //}


    //var estado = $("input[name=estado]:checked").val();
    //var aprobacion = $("input[name=aprobacion]:checked").val();

    //if (invalido) {
    //    alert("invalido");
    //    $("#default-title-0").addClass("current-step");
    //    $("#default-title-1").removeClass("current-step");
    //    $("#btnback").click();
    //}
}

function validationError(control) {
    var validator = $("#wfrmpuesto").validate();
    validator.showErrors({
        control: "<span style='color:red;'>Campo requerido</span>"
    });
}