// document.getElementById('unitSelect').addEventListener('change', function() {
//     let customUnitGroup = document.getElementById('customUnitGroup');
//     if (this.value === '') {
//         customUnitGroup.style.display = 'block';
//     } else {
//         customUnitGroup.style.display = 'none';
//     }
// });

$('#unitSelect').change((e) => {
    let customUnitGroup = $('#customUnitGroup');
    if (e.target.value === '') {
        customUnitGroup.show();
    } else {
        customUnitGroup.hide();
    }
});
