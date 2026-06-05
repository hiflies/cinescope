for (const button of document.querySelectorAll('.password-toggle')) {
    button.addEventListener('click', function () {
        this.previousElementSibling.type = this.previousElementSibling.type === 'text' ? 'password' : 'text';
    });
}