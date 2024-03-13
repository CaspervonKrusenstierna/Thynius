import React, { useEffect, useState } from 'react'
import Header from './components/header/Header'
import MainContentContainer from './components/maincontentcontainer/MainContentContainer'
import Footer from './components/footer/Footer'
import LoginBox from './components/loginbox/LoginBox'
import RegisterBox from './components/registerbox/RegisterBox'
import ForgotPasswordBox from './components/forgotpasswordbox/ForgotPasswordBox'

import "./HomePage.css"
const HomePage = (props) => {
  const [toRender, setToRender] = useState(<></>)

  useEffect(() => {
    function OnExit(){
      setToRender(<><Header loggedIn={props.isLoggedIn} onLoginClick={LoginClick} onRegisterClick={RegisterClick} disableSticky={false}></Header><MainContentContainer/><Footer/></>);
    }
    function ForgotPasswordClick(){
      OnExit();
      setToRender(<div className='HomePage-unfocus'><div className='DarknessEffect'></div><Header loggedIn={props.isLoggedIn} onLoginClick={LoginClick} onRegisterClick={RegisterClick} disableSticky={true}></Header><MainContentContainer/><Footer/><ForgotPasswordBox onExitClick={OnExit}></ForgotPasswordBox></div>)
    }
    function LoginClick(redirect){
      console.log(redirect);
      window.scrollTo(0,0);
      setToRender(<div className='HomePage-unfocus'><div className='DarknessEffect'></div><Header loggedIn={props.isLoggedIn} onLoginClick={LoginClick} onRegisterClick={RegisterClick} disableSticky={true}></Header><MainContentContainer/><Footer/><LoginBox redirect={redirect} onExitClick={OnExit} onForgotPassword={ForgotPasswordClick}></LoginBox></div>)
    }
    function RegisterClick(){
      window.scrollTo(0,0);
      setToRender(<div className='HomePage-unfocus'><div className='DarknessEffect'></div><Header loggedIn={props.isLoggedIn} onLoginClick={LoginClick} onRegisterClick={RegisterClick} disableSticky={true}></Header><MainContentContainer/><Footer/><RegisterBox onExitClick={OnExit}></RegisterBox></div>)
    }
    setToRender(<><Header loggedIn={props.isLoggedIn} onLoginClick={LoginClick} onRegisterClick={RegisterClick} disableSticky={false}></Header><Footer/></>)
  }, [])


  return toRender
}


export default HomePage

