/* eslint-disable react-hooks/rules-of-hooks */
import React, { useRef, useState } from 'react'
import "./RegisterBox.css"
import "../../../shared/styles/HomePageBox.css"
import useRegister from './useRegister';
import { useNavigate } from "react-router-dom";
import { AnchorButton, Input, SubmitButton } from '../../../shared/components/homepage';
import HomePageBoxHeader from '../../../shared/components/dashboard/homepageboxheader/HomePageBoxHeader';
import { EnvelopeImg, UnlockImg } from '../../../shared/assets';

const validateEmail = (email) => {
    return String(email)
      .toLowerCase()
      .match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );
  };

const RegisterBox = () => {
    const [error, setError] = useState();
    const Username = useRef("");
    const Password = useRef("");
    const Email = useRef("");
    const ConfirmPassword = useRef("");
    const navigate = useNavigate();

    async function onSubmit() {
        let submittedPassword = Password.current;
        if (submittedPassword != ConfirmPassword.current) {
            setError("Dina lösenord matchar inte.")
            return;
        }
        if (!validateEmail(Email.current)){
            setError("Var vänlig och ange en giltig email.");
            return;
        }
        if (submittedPassword < 8){
            setError("Ditt lösenord måste innehålla minst 8 karaktärer.")
            return;
        }
        if (submittedPassword > 24){
            setError("Ditt lösenord får inte innehålla mer än 24 karaktärer")
            return;
        }
        if(!/\d/.test(submittedPassword)){
            setError("Ditt lösenord måste innehålla minst en siffra.")
            return;
        }
        if(!/[A-Z]/.test(submittedPassword)){
            setError("Ditt lösenord måste innehålla minst en stor bokstav.")
            return;
        }
        if(!/[a-z]/.test(submittedPassword)){
            setError("Ditt lösenord måste innehålla minst en liten bokstav.")
            return;
        }

        const response = await useRegister(Password.current, Email.current);
        if (response.ok) {
            navigate("/")
        }
        response.json().then(res => { setError(JSON.stringify(res.error)) })
    }

        return (
            <div className='HomePageBox RegisterBox'>
                <HomePageBoxHeader></HomePageBoxHeader>
                <div className='InputContainer'>
                    <p className="text-base text-red-500 h-5">{error}</p>
                    <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                    <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label="Lösenord" img={UnlockImg}></Input>
                    <Input hide={true} onChange={(e) => { ConfirmPassword.current = e.target.value }} label="Bekräfta lösenord" img={UnlockImg}></Input>
                    <div className='HomePageBox-BottomContainer'>
                        <AnchorButton text="Har du redan ett konto?" onClick={() => { navigate("/login") }}></AnchorButton>
                    </div>
                    <SubmitButton onClick={onSubmit} text="Registrera"></SubmitButton>
                </div>
            </div>
        )
}

export default RegisterBox