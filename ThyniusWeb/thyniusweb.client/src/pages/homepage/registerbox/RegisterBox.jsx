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
import validateEmail from './utils/validateMail';


const RegisterBox = () => {
    const [t, i18] = useTranslation();
    const [error, setError] = useState();
    const formValues = useRef({
        password: '',
        email: '',
        confirmPassword: '',
      });
    const navigate = useNavigate();

    async function onSubmit() {
        let submittedForm = formValues.current;
        if (submittedForm.password != submittedForm.confirmPassword) {
            setError(t("Your passwords dont match."))
            return;
        }
        if (!validateEmail(submittedForm.email)){
            setError(t("Please submit a valid email address."));
            return;
        }
        let passwordError = getPasswordError(submittedForm.password);
        if(passwordError){
            setError(t(passwordError));
            return;
        }
        const response = await useRegister(submittedForm.password, submittedForm.email);
        if (response.ok) {
            navigate("/")
        }
        response.json().then((res) => {
            setError(res.error || t("An unexpected error occurred."));
        });
    }

        return (
            <div className='HomePageBox RegisterBox'>
                <HomePageBoxHeader></HomePageBoxHeader>
                <div className='InputContainer'>
                    <p className="text-base text-red-500 min-h-5">{error}</p>
                    <Input onChange={(e) => { formValues.current.email = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                    <Input hide={true} onChange={(e) => { formValues.current.password = e.target.value }} label={t("Password")} img={UnlockImg}></Input>
                    <Input hide={true} onChange={(e) => { formValues.current.confirmPassword = e.target.value }} label={t("Confirm password")} img={UnlockImg}></Input>
                    <div className='HomePageBox-BottomContainer'>
                        <AnchorButton text={t("Already have an account?")} onClick={() => { navigate("/login") }}></AnchorButton>
                    </div>
                    <SubmitButton onClick={onSubmit} text={t("Register")}></SubmitButton>
                </div>
            </div>
        )
}

export default RegisterBox