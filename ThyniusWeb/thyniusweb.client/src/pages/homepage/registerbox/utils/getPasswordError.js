export default getPasswordError = (submittedPassword) => {
    if (submittedPassword < 8){
        return "Your password needs to contain 8 characters at minimum."
    }
    if (submittedPassword > 24){
        return "Your password cannot contain more than 24 characters."
    }
    if(!/\d/.test(submittedPassword)){
        return "Your password needs to contain one numerical character."
    
    }
    if(!/[A-Z]/.test(submittedPassword)){
        return "Your password needs to contain atleast one capital letter."
    }
    if(!/[a-z]/.test(submittedPassword)){
        return "Your password needs to contain atleast one lowercase letter."
    }
}