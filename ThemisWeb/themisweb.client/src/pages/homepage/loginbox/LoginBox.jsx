/* eslint-disable react-hooks/rules-of-hooks */
import React, { useContext, useState } from 'react'
import "../../../shared/styles/HomePageBox.css"
import "./LoginBox.css"
import { Input, SubmitButton, CheckBoxButton, AnchorButton} from "../../../shared/components/homepage"
import useLogin from './useLogin';
import { useNavigate } from "react-router-dom";
import { X } from 'lucide-react';
import { sessionInfoContext } from '../../../App'
import { EnvelopeImg, UnlockImg } from '../../../shared/assets'
import HomePageBoxHeader from '../../../shared/components/dashboard/homepageboxheader/HomePageBoxHeader';

const LoginBox = () => {
    const sessionInfo = useContext(sessionInfoContext);
    const [error, setError] = useState();
    const Email = useRef("");
    const Password = useRef("");
    const RememberMe = useRef(false);
    const navigate = useNavigate();


    async function onLoginSubmit() {
        const response = await useLogin(Email.current, Password.current, RememberMe.current);

        if (response.ok) {
            sessionInfo.updateSessionInfo();
            navigate("/dashboard/home")
        }
        response.json().then(res => { setError(JSON.stringify(res.title)) })
    }

    return (
        <div className='HomePageBox LoginBox'>
            <HomePageBoxHeader></HomePageBoxHeader>
            <div className='InputContainer'>
                <p className="text-base text-red-500 h-5">{error}</p>
                <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                <Input hide={true} onChange={(e) => { Password.current = e.target.value }} label="Lösenord" img={UnlockImg}></Input>
                <div className='HomePageBox-BottomContainer'>
                    <CheckBoxButton onChange={(value) => { RememberMe.current = value }} text="Kom ihåg mig"></CheckBoxButton>
                    <AnchorButton text="Glömt ditt lösenord?" onClick={() => { navigate("forgotpassword") }}></AnchorButton>
                </div>
                <SubmitButton onClick={onLoginSubmit} text="Logga in"></SubmitButton>
            </div>
        </div>
    )
}

export default LoginBox