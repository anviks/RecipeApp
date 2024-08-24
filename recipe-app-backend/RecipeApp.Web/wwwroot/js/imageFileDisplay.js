function displayImage(input) {
    const previewImage = document.getElementById('previewImage');
    
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
            previewImage.src = e.target.result;
            previewImage.style.display = 'block';
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        previewImage.src = "#";
        previewImage.style.display = 'none';
    }
}
