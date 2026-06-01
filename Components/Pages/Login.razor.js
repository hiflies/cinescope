const authToggle = document.getElementById('auth-toggle');
const authTitle = document.getElementById('auth-title');
const authSubtitle = document.getElementById('auth-subtitle');
const submitBtn = document.getElementById('submit-btn');
const toggleText = document.getElementById('toggle-text');
const confirmPass = document.getElementById('confirm-password-container');
const rememberMe = document.getElementById('remember-me-container');
const forgotPass = document.getElementById('forgot-password');

let isLogin = true;

authToggle.addEventListener('click', () => {
    isLogin = !isLogin;

    if (isLogin) {
        authTitle.innerText = 'Welcome Back';
        authSubtitle.innerText = 'Enter your details to access your cinematic universe.';
        submitBtn.querySelector('span:first-child').innerText = 'Sign In';
        toggleText.innerHTML = `Don't have an account? <button id="auth-toggle-inner" class="text-primary font-bold hover:underline">Create Account</button>`;
        confirmPass.classList.add('hidden');
        rememberMe.classList.remove('hidden');
        forgotPass.classList.remove('hidden');
    } else {
        authTitle.innerText = 'Join CineScope';
        authSubtitle.innerText = 'Create your account and start discovering amazing cinema.';
        submitBtn.querySelector('span:first-child').innerText = 'Create Account';
        toggleText.innerHTML = `Already have an account? <button id="auth-toggle-inner" class="text-primary font-bold hover:underline">Sign In</button>`;
        confirmPass.classList.remove('hidden');
        rememberMe.classList.add('hidden');
        forgotPass.classList.add('hidden');
    }

    // Re-bind listener because innerHTML wipes it
    document.getElementById('auth-toggle-inner').addEventListener('click', () => authToggle.click());
});

// Simple animation on form submit
document.getElementById('auth-form').addEventListener('submit', (e) => {
    e.preventDefault();
    const btn = document.getElementById('submit-btn');
    btn.innerHTML = '<span class="material-symbols-outlined animate-spin">progress_activity</span>';
    setTimeout(() => {
        btn.innerHTML = '<span>Success!</span>';
        btn.classList.replace('bg-primary-container', 'bg-tertiary-container');
    }, 1500);
});