import React, { useState } from 'react'
import {Input, SubmitButton, ErrorMessage, ExitButton, VerificationMessage} from "../common/components"
import {EnvelopeImg} from  "../common/assets"
import "./ForgotPasswordBox.css"

let Email = "";

const ForgotPasswordBox = (props) => {
    const [hasSubmitted, setHasSubmitted] = useState(false);

    async function onSubmit(){
      const response = await fetch("http://127.0.0.1:3000/forgotpassword", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
          "Content-Type": "application/json",
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
        body: JSON.stringify({"email": Email})
      }).then((res) => res.json());
  

      setHasSubmitted(true);
    }
    if(hasSubmitted){
        return (
            <div className='ForgotPasswordBox'>
                <div className='ExitButtonContainer'>
                  <ExitButton onClick={props.onExitClick}></ExitButton>
                </div>
                <div className='inputcontainer-forgotpassword'>
                  <VerificationMessage text="Gå in på din email för vidare instruktioner."></VerificationMessage>
                  <Input onChange={(e) => {Email = e.target.value}} label="Email" img={EnvelopeImg}></Input>
                  <SubmitButton onClick={onSubmit} text="Bekräfta"></SubmitButton>
                </div>
            </div>
          );
    }
    return (
        <div className='ForgotPasswordBox'>
            <div className='ExitButtonContainer'>
              <ExitButton onClick={props.onExitClick}></ExitButton>
            </div>
            <div className='inputcontainer-forgotpassword'>
              <Input onChange={(e) => {Email = e.target.value}} label="Email" img={EnvelopeImg}></Input>
              <SubmitButton onClick={onSubmit} text="Bekräfta"></SubmitButton>
            </div>
        </div>
      );
}

export default ForgotPasswordBox