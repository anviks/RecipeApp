document.getElementById('add-step').addEventListener('click', function() {
    const instructionFields = document.getElementById('steps');
    const input = document.createElement('textarea');
    const label = document.createElement('label');
    const index = instructionFields.children.length / 2; // Get the current number of instruction fields

    if (index === 1) {
        document.getElementById('remove-step').disabled = false;
    }

    input.setAttribute('class', 'form-control');
    input.setAttribute('id', `Instructions_${index}_`); // Set the id attribute
    input.setAttribute('name', 'Instructions[' + index + ']'); // Set the name attribute

    label.setAttribute('class', 'control-label');
    label.setAttribute('for', `Instructions_${index}_`);
    label.appendChild(document.getElementById('localizedStepLabel').cloneNode(true));
    label.innerHTML += ' ' + (index + 1);

    instructionFields.appendChild(label);
    instructionFields.appendChild(input);
});

document.getElementById('remove-step').addEventListener('click', function(e) {
    const instructionFields = document.getElementById('steps');
    const index = instructionFields.children.length / 2; // Get the current number of instruction fields

    if (index <= 2) {
        this.disabled = true;
    }

    if (index > 0) {
        instructionFields.removeChild(instructionFields.lastElementChild);
        instructionFields.removeChild(instructionFields.lastElementChild);
    }
});
