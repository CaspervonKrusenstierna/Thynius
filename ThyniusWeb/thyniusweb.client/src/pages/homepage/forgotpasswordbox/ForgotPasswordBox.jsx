/* eslint-disable react-hooks/rules-of-hooks */
import React, { useContext, useRef, useState } from 'react'
import "../../../shared/styles/HomePageBox.css"
import "./ForgotPasswordBox.css"
import HomePageBoxHeader from '../../../shared/components/dashboard/homepageboxheader/HomePageBoxHeader';
import { Input, SubmitButton } from '../../../shared/components/homepage';
import { EnvelopeImg } from '../../../shared/assets';
import { useTranslation } from 'react-i18next';

const ForgotPasswordBox = () => {
    const [t, i18] = useTranslation();
    const Email = useRef("");
    const [hasSent, setHasSent] = useState(false);
    async function onSubmit() {
        setHasSent(true);
    }
    return (
        <div className='HomePageBox ForgotPasswordBox'>
            <HomePageBoxHeader></HomePageBoxHeader>
            {hasSent ? <p className='w-[80%] h-[30px] font-bold'>Om e-post adressen som du har angett är giltig, så har vi skickat ett verifikations meddelande till dig.</p> : <div></div>}
            <div className='InputContainer pt-3 gap-2'>
                <Input onChange={(e) => { Email.current = e.target.value }} label="Email" img={EnvelopeImg}></Input>
                <SubmitButton onClick={onSubmit} text={t("Send Verification")}></SubmitButton>
            </div>
        </div>
    )
}

export default ForgotPasswordBox