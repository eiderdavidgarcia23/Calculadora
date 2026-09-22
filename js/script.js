const pantalla = document.getElementById('pantalla');

function agregar(valor) {
    if (pantalla.value === 'Error' || pantalla.value === 'No se puede dividir entre cero') {
        pantalla.value = '';
    }
    pantalla.value += valor;
}

function limpiarTodo() {
    pantalla.value = '';
}

function limpiarEntrada() {
    pantalla.value = '';
}

function retroceso() {
    if (pantalla.value !== 'Error' && pantalla.value !== 'No se puede dividir entre cero') {
        pantalla.value = pantalla.value.slice(0, -1);
    } else {
        pantalla.value = '';
    }
}

function calcular() {
    try {
        if (pantalla.value.trim() !== '') {
            let resultado = Function('"use strict";return (' + pantalla.value + ')')();
            if (!isFinite(resultado)) {
                pantalla.value = 'No se puede dividir entre cero';
            } else {
                pantalla.value = resultado;
            }
        }
    } catch (e) {
        pantalla.value = 'Error';
    }
}
