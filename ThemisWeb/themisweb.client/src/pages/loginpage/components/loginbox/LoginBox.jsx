/* eslint-disable react-hooks/rules-of-hooks */
import React, { useRef, useState } from 'react'
import "./LoginBox.css"
import { Input, SubmitButton, ErrorMessage, CheckBoxButton, AnchorButton, ExitButton } from "../../../../shared/components"
import { UserImg, UnlockImg } from "../../../../shared/assets"
import useLogin from './useLogin';
import { useNavigate } from "react-router-dom";

const LoginBox = () => {
    const [error, setError] = useState();
    const Email = useRef("");
    const Password = useRef("");
    const RememberMe = useRef(false);
    const navigate = useNavigate();
    async function onLoginSubmit() {
        const response = await useLogin(Email.current, Password.current, RememberMe.current);

        if (response.ok) {
            navigate("/dashboard")
        }
        response.json().then(res => { setError(JSON.stringify(res.title)) })
    }

    return (
        <div className='LoginBox'>
            <div className='ExitButtonContainer'>
                <ExitButton onClick={() => { navigate("/") }}></ExitButton>
            </div>
            <div className='inputcontainer-login'>
                {error && <ErrorMessage message={error}></ErrorMessage>}
                <Input onChange={(e) => { Email.current = e.target.value }} label="Användarnamn" img={UserImg}></Input>
                <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label="Lösenord" img={UnlockImg}></Input>
                <div className='LoginBox-BottomContainer'>
                    <CheckBoxButton onChange={(value) => { RememberMe.current = value }} text="Kom ihåg mig"></CheckBoxButton>
                    <AnchorButton text="Glömt ditt lösenord?" onClick={() => { navigate("forgotpassword") }}></AnchorButton>
                </div>
                <SubmitButton onClick={onLoginSubmit} text="Logga in"></SubmitButton>
            </div>
        </div>
    )
}

export default LoginBox