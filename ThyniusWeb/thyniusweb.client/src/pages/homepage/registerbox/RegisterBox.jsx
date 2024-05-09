/* eslint-disable react-hooks/rules-of-hooks */
import React, { useRef, useState } from 'react'
import "./RegisterBox.css"
import "../../../shared/styles/HomePageBox.css"
import useRegister from './useRegister';
import { useNavigate } from "react-router-dom";
import { AnchorButton, Input, SubmitButton } from '../../../shared/components/homepage';
import HomePageBoxHeader from '../../../shared/components/dashboard/homepageboxheader/HomePageBoxHeader';
import { EnvelopeImg, UnlockImg } from '../../../shared/assets';
import { useTranslation } from 'react-i18next';

const validateEmail = (email) => {
    return String(email)
      .toLowerCase()
      .match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );
  };

const RegisterBox = () => {
    const [t, i18] = useTranslation();
    const [error, setError] = useState();
    const Username = useRef("");
    const Password = useRef("");
    const Email = useRef("");
    const ConfirmPassword = useRef("");
    const navigate = useNavigate();

    async function onSubmit() {
        let submittedPassword = Password.current;
        if (submittedPassword != ConfirmPassword.current) {
            setError(t("Your passwords dont match."))
            return;
        }
        if (!validateEmail(Email.current)){
            setError(t("Please submit a valid email address."));
            return;
        }
        if (submittedPassword < 8){
            setError(t("Your password needs to contain 8 characters at minimum."))
            return;
        }
        if (submittedPassword > 24){
            setError(t("Your password cannot contain more than 24 characters."))
            return;
        }
        if(!/\d/.test(submittedPassword)){
            setError(t("Your password needs to contain one numerical character."))
            return;
        }
        if(!/[A-Z]/.test(submittedPassword)){
            setError(t("Your password needs to contain atleast one capital letter."))
            return;
        }
        if(!/[a-z]/.test(submittedPassword)){
            setError(t("Your password needs to contain atleast one lowercase letter."))
            return;
        }

        const response = await useRegister(Password.current, Email.current);
        if (response.ok) {
            navigate("/")
        }
        response.json().then(res => { setError(JSON.stringify(res.error).replaceAll('"', "")) })
    }

        return (
            <div className='HomePageBox RegisterBox'>
                <HomePageBoxHeader></HomePageBoxHeader>
                <div className='InputContainer'>
                    <p className="text-base text-red-500 min-h-5">{error}</p>
                    <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                    <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label={t("Password")} img={UnlockImg}></Input>
                    <Input hide={true} onChange={(e) => { ConfirmPassword.current = e.target.value }} label={t("Confirm password")} img={UnlockImg}></Input>
                    <div className='HomePageBox-BottomContainer'>
                        <AnchorButton text={t("Already have an account?")} onClick={() => { navigate("/login") }}></AnchorButton>
                    </div>
                    <SubmitButton onClick={onSubmit} text={t("Register")}></SubmitButton>
                </div>
            </div>
        )
}

export default RegisterBox