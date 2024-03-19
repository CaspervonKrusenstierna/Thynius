/* eslint-disable react-hooks/rules-of-hooks */
import React, { useRef, useState } from 'react'
import "./RegisterBox.css"
import { Input, SubmitButton, ErrorMessage, ExitButton} from "../../../../shared/components"
import { UserImg, UnlockImg, EnvelopeImg } from "../../../../shared/assets"
import useRegister from './useRegister';
import { useNavigate } from "react-router-dom";

const RegisterBox = () => {
    const [error, setError] = useState();
    const Username = useRef("");
    const Password = useRef("");
    const Email = useRef("");
    const ConfirmPassword = useRef("");
    const navigate = useNavigate();

    async function onSubmit() {
        if (Password.current != ConfirmPassword.current) {
            setError("Passwords do not match.")
            return;
        }
        const response = await useRegister(Username.current, Password.current, Email.current);


        if (response.ok) {
            navigate("/")
        }
        response.json().then(res => { setError(JSON.stringify(res.errors)) })
    }

        return (
            <div className='RegisterBox'>
                <div className='ExitButtonContainer'>
                    <ExitButton onClick={() => { navigate("/") }}></ExitButton>
                </div>
                <div className='inputcontainer-register'>
                    {error && <ErrorMessage message={error}></ErrorMessage>}
                    <Input onChange={(e) => { Username.current = e.target.value }} label="Användarnamn" img={UserImg}></Input>
                    <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                    <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label="Lösenord" img={UnlockImg}></Input>
                    <Input hide={true} onChange={(e) => { ConfirmPassword.current = e.target.value }} label="Bekräfta lösenord" img={UnlockImg}></Input>
                    <SubmitButton onClick={onSubmit} text="Registrera"></SubmitButton>
                </div>
            </div>
        )
}

export default RegisterBox