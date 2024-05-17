// document.getElementById('add-step').addEventListener('click', function() {
//     const instructionFields = document.getElementById('steps');
//     const input = document.createElement('textarea');
//     const label = document.createElement('label');
//     const index = instructionFields.children.length / 2; // Get the current number of instruction fields
//
//     if (index === 1) {
//         document.getElementById('remove-step').disabled = false;
//     }
//
//     input.setAttribute('class', 'form-control');
//     input.setAttribute('id', `Instructions_${index}_`); // Set the id attribute
//     input.setAttribute('name', 'Instructions[' + index + ']'); // Set the name attribute
//
//     label.setAttribute('class', 'control-label');
//     label.setAttribute('for', `Instructions_${index}_`);
//     label.innerText = document.getElementById('localizedStepLabel').innerText + ' ' + (index + 1);
//
//     instructionFields.appendChild(label);
//     instructionFields.appendChild(input);
// });

$('#add-step').click(() => {
    const instructionFields = $('#steps');
    const index = instructionFields.children().length / 2; // Get the current number of instruction fields

    if (index === 1) {
        $('#remove-step').prop('disabled', false);
    }

    const input = $('<textarea>')
    .addClass('form-control')
    .attr({
        id: `Instructions_${index}_`,
        name: `Instructions[${index}]`
    });

    const label = $('<label>')
    .addClass('control-label')
    .attr('for', `Instructions_${index}_`)
    .text($('#localizedStepLabel').text() + ' ' + (index + 1));

    instructionFields.append(label);
    instructionFields.append(input);
});

// document.getElementById('remove-step').addEventListener('click', function(e) {
//     const instructionFields = document.getElementById('steps');
//     const index = instructionFields.children.length / 2; // Get the current number of instruction fields
//
//     if (index <= 2) {
//         this.disabled = true;
//     }
//
//     if (index > 0) {
//         instructionFields.removeChild(instructionFields.lastElementChild);
//         instructionFields.removeChild(instructionFields.lastElementChild);
//     }
// });

$('#remove-step').click((e) => {
    const instructionFields = $('#steps');
    const index = instructionFields.children().length / 2; // Get the current number of instruction fields

    if (index <= 2) {
        e.target.disabled = true;
    }

    if (index > 0) {
        instructionFields.children().last().remove();
        instructionFields.children().last().remove();
    }
});
