var Password = {
    Length: 16,
    ContainLowerChar: true,
    ContainUpperChar: true,
    ContainSpecialChar: true,
    ContainNumberChar: true,
    Generate: function () {
        let result = "";
        for (let i = 0; i < this.Length; i++) {
            let char = String.fromCharCode(Math.floor(Math.random() * 87 + 35));
            result += char;
        }
        if (!hasLowerCase(result))
            result += String.fromCharCode(Math.floor(Math.random() * 25 + 97));
        if (!hasUpperCase(result))
            result += String.fromCharCode(Math.floor(Math.random() * 25 + 65));
        if (!hasNumber(result))
            result += String.fromCharCode(Math.floor(Math.random() * 10 + 48));
        if (!hasSpecialChar(result))
            result += String.fromCharCode(Math.floor(Math.random() * 10 + 35));

        return result;
    }
}
function hasLowerCase(str) {
    return (/[a-z]/.test(str));
}
function hasUpperCase(str) {
    return (/[A-Z]/.test(str));
}
function hasNumber(str) {
    return (/[0-9]/.test(str));
}
function hasSpecialChar(str) {
    return (/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(str));
}