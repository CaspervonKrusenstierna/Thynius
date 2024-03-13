import React, { useState } from 'react'
import "./LoginBox.css"
import {Input, SubmitButton, ErrorMessage, CheckBoxButton, AnchorButton, ExitButton} from "../common/components"
import {UserImg, UnlockImg} from  "../common/assets"

let Username = "";
let Password = "";
let RememberMe = false;

const LoginBox = (props) => {
    const [error, setError] = useState();
  
    async function onSubmit(){
      const response = await fetch("http://127.0.0.1:3000/login", {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
          "Content-Type": "application/json",
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
        body: JSON.stringify({"name": Username, "password": Password, "remember": RememberMe})
      }).then((res) => res.json());
  
      if(response.success){
        window.location.href = props.redirect;
      }
      setError(response.message);
    }

    if(Error !== ""){
      return (
        <div className='LoginBox'>
            <div className='ExitButtonContainer'>
              <ExitButton onClick={props.onExitClick}></ExitButton>
            </div>
            <div className='inputcontainer-login'>
              <ErrorMessage message={error}></ErrorMessage>
              <Input onChange={(e) => {Username = e.target.value}} label="Användarnamn" img={UserImg}></Input>
              <Input hide={true} onChange={(e) => {Password = e.target.value}} label="Lösenord" img={UnlockImg}></Input>
              <div className='LoginBox-BottomContainer'>
                <CheckBoxButton onChange={(e) => {}} text="Kom ihåg mig"></CheckBoxButton>
                <AnchorButton text="Glömt ditt lösenord?" onClick={props.onForgotPassword}></AnchorButton>
              </div>
              <SubmitButton onClick={onSubmit} text="Logga in"></SubmitButton>
            </div>
        </div>
      )
    }
  
    return (
      <div className='LoginBox'>
          <div className='ExitButtonContainer'>
              <ExitButton onClick={props.onExitClick}></ExitButton>
          </div>
          <div className='inputcontainer-login'>
            <Input onChange={(e) => {Username = e.target.value}} label="Användarnamn" img={UserImg}></Input>
            <Input hide={true} onChange={(e) => {Password = e.target.value}} label="Lösenord" img={UnlockImg}></Input>
            <div className='LoginBox-BottomContainer'>
                <CheckBoxButton></CheckBoxButton>
                <AnchorButton text="Glömt ditt lösenord?" onClick={props.onForgotPassword}></AnchorButton>
            </div>
            <SubmitButton onClick={onSubmit} text="Logga in"></SubmitButton>
          </div>
      </div>
    )
}

export default LoginBox