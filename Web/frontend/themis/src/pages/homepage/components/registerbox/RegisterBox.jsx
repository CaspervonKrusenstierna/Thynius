import React, { useState } from 'react'
import "./RegisterBox.css"
import {Input, SubmitButton, ErrorMessage, ExitButton} from "../common/components"
import {UserImg, UnlockImg, EnvelopeImg} from "../common/assets"

let Username = "";
let Email = "";
let Password = "";
let ConfirmPassword = "";

const RegisterBox = (props) => {
  const [error, setError] = useState();

  async function onSubmit(){
    if(Password != ConfirmPassword){
      setError("Passwords do not match.")
      return;
    }

    const response = await fetch("http://127.0.0.1:3000/register", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "same-origin",
      headers: {
        "Content-Type": "application/json",
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify({"name": Username, "email": Email, "password": Password})
    }).then((res) => res.json());

    if(response.success){
      props.onExitClick();
    }
    setError(response.message);
  }

  if(Error !== ""){
    return (
      <div className='RegisterBox'>
          <div className='ExitButtonContainer'>
            <ExitButton onClick={props.onExitClick}></ExitButton>
          </div>
          <div className='inputcontainer-register'>
            <ErrorMessage message={error}></ErrorMessage>
            <Input onChange={(e) => {Username = e.target.value}} label="Användarnamn" img={UserImg}></Input>
            <Input onChange={(e) => {Email = e.target.value}} label="Email" img={EnvelopeImg}></Input>
            <Input hide={true} onChange={(e) => {Password = e.target.value}} label="Lösenord" img={UnlockImg}></Input>
            <Input hide={true} onChange={(e) => {ConfirmPassword = e.target.value}} label="Bekräfta lösenord" img={UnlockImg}></Input>
            <SubmitButton onClick={onSubmit} text="Registrera"></SubmitButton>
          </div>
      </div>
    )
  }

  return (
    <div className='RegisterBox'>
        <div className='ExitButtonContainer'>
            <ExitButton onClick={props.onExitClick}></ExitButton>
        </div>
        <div className='inputcontainer-register'>
          <Input onChange={(e) => {Username = e.target.value}} label="Användarnamn" img={UserImg}></Input>
          <Input onChange={(e) => {Email = e.target.value}} label="Email" img={EnvelopeImg}></Input>
          <Input hide={true} onChange={(e) => {Password = e.target.value}} label="Lösenord" img={UnlockImg}></Input>
          <Input hide={true} onChange={(e) => {ConfirmPassword = e.target.value}} label="Bekräfta lösenord" img={UnlockImg}></Input>
          <SubmitButton onClick={onSubmit} text="Registrera"></SubmitButton>
        </div>
    </div>
  )
}

export default RegisterBox