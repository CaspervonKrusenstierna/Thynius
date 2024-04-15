/* eslint-disable react-hooks/rules-of-hooks */
import React, { useContext, useState } from 'react'
import "../../../shared/styles/HomePageBox.css"
import "./ForgotPasswordBox.css"
import useForgotPassword from './useForgotPassword';
import { EnvelopeImg} from '../../../shared/assets'
import HomePageBoxHeader from '../../../shared/components/dashboard/homepageboxheader/HomePageBoxHeader';
import useScrollTop from '../useScrollTop';

const ForgotPasswordBox = () => {
    const Email = useRef("");
    useScrollTop();

    async function onSubmit() {
        const response = await useLogin(Email.current, Password.current, RememberMe.current);

        if (response.ok) {
            sessionInfo.updateSessionInfo();
            navigate("/dashboard/home")
        }
        response.json().then(res => { setError(JSON.stringify(res.title)) })
    }

    return (
        <div className='HomePageBox ForgotPasswordBox'>
            <HomePageBoxHeader></HomePageBoxHeader>
            <div className='InputContainer'>
                
            </div>
        </div>
    )
}

export default ForgotPasswordBox