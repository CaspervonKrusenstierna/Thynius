import React, { useEffect, useState } from 'react'
import {NavBarButtonRedirect, NavBarLogoRedirect, NavBarRedirect} from "./components"
import "./Header.css"
import NavBarConditionalRedirect from './components/NavBarConditionalRedirect/NavBarConditionalRedirect';

let BigHeaderContent;
let SmallHeaderContent;
let SmallHeaderThreshhold = 700;
let previousSize;

const Header = (props) => {
  const [ClassName, setClassName] = useState("Header");
  const [HeaderContent, setHeaderContent] = useState([0]);

  useEffect(() =>{
    function OnScroll(){
      if(props.disableSticky){
        return;
      }
      let header = document.getElementById("Header")
      if(window.scrollY >= 110){
        setClassName("Header Sticky")
      }
      else{
        setClassName("Header")
      }
    }
    function OnResize(){
      let HeaderContentBuffer = [];
      if(window.innerWidth >= SmallHeaderThreshhold && previousSize < SmallHeaderThreshhold){
        previousSize = window.innerWidth;
        setHeaderContent(BigHeaderContent);
      }
      else if(window.innerWidth < SmallHeaderThreshhold && previousSize >= SmallHeaderThreshhold){
        previousSize = window.innerWidth;
        setHeaderContent(SmallHeaderContent);
      }
    }
    function OnPricesClick(){
      if(!props.loggedIn){
        props.onLoginClick("/dashboard/payment");
      }
      else{
        window.location.href = "/dashboard/payment"
      }
    }

  document.onscroll = OnScroll;
  window.onresize = OnResize;

  if(props.loggedIn){
    BigHeaderContent = <>
    <NavBarLogoRedirect></NavBarLogoRedirect>
    <NavBarConditionalRedirect text="Priser" onClick={OnPricesClick}></NavBarConditionalRedirect>
    <NavBarRedirect redirect="Frågor" href="#AccordionSection"></NavBarRedirect>
    <NavBarButtonRedirect text="Dashboard" onClick={() => {window.location.href = "/dashboard"}}></NavBarButtonRedirect>
    </>
    SmallHeaderContent = <>
    <NavBarLogoRedirect></NavBarLogoRedirect>
    <NavBarButtonRedirect text="Dashboard" onClick={() => {window.location.href = "/dashboard"}}></NavBarButtonRedirect>
    </>
  }
  else{
    BigHeaderContent = <>
    <NavBarLogoRedirect></NavBarLogoRedirect>
    <NavBarConditionalRedirect text="Priser" onClick={OnPricesClick}></NavBarConditionalRedirect>
    <NavBarRedirect redirect="Frågor" href="#AccordionSection"></NavBarRedirect>
    <div className='Header-ButtonContainer'>
    <NavBarButtonRedirect onClick={() => {props.onLoginClick("/dashboard")}} text="Login"></NavBarButtonRedirect>
    <NavBarButtonRedirect onClick={props.onRegisterClick} text="Register"></NavBarButtonRedirect>
    </div>
    </>
    SmallHeaderContent = <>
    <NavBarLogoRedirect></NavBarLogoRedirect>
    <div className='Header-ButtonContainer'>
    <NavBarButtonRedirect onClick={() => {props.onLoginClick("/dashboard")}} text="Login"></NavBarButtonRedirect>
    <NavBarButtonRedirect onClick={props.onRegisterClick} text="Register"></NavBarButtonRedirect>
    </div>
    </>
  }

  previousSize = window.innerWidth
  if(window.innerWidth >= SmallHeaderThreshhold){
    setHeaderContent(BigHeaderContent);
  }
  else{
    setHeaderContent(SmallHeaderContent);
  }
 },[])

 return (<div className='Header-Container'><div className={ClassName}>{HeaderContent}</div></div>)
}

export default Header
