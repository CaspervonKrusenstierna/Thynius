/* eslint-disable react-hooks/rules-of-hooks */
import React, { useRef, useState } from 'react'
import "./RegisterBox.css"
import { AnchorButton, Input, SubmitButton} from "../../../../shared/components/homepage"
import { UserImg, UnlockImg, EnvelopeImg } from "../../../../shared/assets"
import useRegister from './useRegister';
import { useNavigate } from "react-router-dom";
import { X } from 'lucide-react';
import useInitializeAccount from './useInitializeAccount';

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
        const response = await useRegister(Password.current, Email.current);
        if (response.ok) {
            useInitializeAccount();
            navigate("/")
        }
        response.json().then(res => { setError(JSON.stringify(res.error)) })
    }

        return (
            <div className='RegisterBox'>
                <div className='ExitButtonContainer'>
                    <button className='h-10 w-10' onClick={() => {navigate("/")}}><X className='w-full h-full'></X></button>
                </div>
                <div className='inputcontainer-register'>
                    <p className="text-base text-red-500 h-5">{error}</p>
                    <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                    <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label="Lösenord" img={UnlockImg}></Input>
                    <Input hide={true} onChange={(e) => { ConfirmPassword.current = e.target.value }} label="Bekräfta lösenord" img={UnlockImg}></Input>
                    <div className='RegisterBox-BottomContainer'>
                        <AnchorButton text="Har du redan ett konto?" onClick={() => { navigate("/login") }}></AnchorButton>
                    </div>
                    <SubmitButton onClick={onSubmit} text="Registrera"></SubmitButton>
                </div>
            </div>
        )
}

export default RegisterBox