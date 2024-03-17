import React, { useEffect, useState } from 'react'
import { NavBarButtonRedirect, NavBarLogoRedirect, NavBarRedirect, NavBarConditionalRedirect } from "./components"
import { useNavigate } from "react-router-dom";
import "./Header.css"

let BigHeaderContent;
let SmallHeaderContent;
let SmallHeaderThreshhold = 700;
let previousSize;

const Header = (props) => {
  const [GroupName, setGroupName] = useState("Header");
  const [HeaderContent, setHeaderContent] = useState([0]);
    const navigate = useNavigate();

    useEffect(() => {
    function OnScroll(){
      if(props.disableSticky){
        return;
      }
      if(window.scrollY >= 110){
        setGroupName("Header Sticky")
      }
      else{
        setGroupName("Header")
      }
    }
    function OnResize(){
      if(window.innerWidth >= SmallHeaderThreshhold && previousSize < SmallHeaderThreshhold){
        previousSize = window.innerWidth;
        setHeaderContent(BigHeaderContent);
      }
      else if(window.innerWidth < SmallHeaderThreshhold && previousSize >= SmallHeaderThreshhold){
        previousSize = window.innerWidth;
        setHeaderContent(SmallHeaderContent);
      }
    }

  document.onscroll = OnScroll;
  window.onresize = OnResize;

  if(props.loggedIn){
    BigHeaderContent = <>
        <NavBarLogoRedirect></NavBarLogoRedirect>
        <NavBarButtonRedirect text="Dashboard" onClick={() => { navigate("/dashboard") }}></NavBarButtonRedirect>
    </>
    SmallHeaderContent = <>
        <NavBarLogoRedirect></NavBarLogoRedirect>
        <NavBarButtonRedirect text="Dashboard" onClick={() => { navigate("/dashboard") }}></NavBarButtonRedirect>
    </>
  }
  else{
    BigHeaderContent = <>
        <NavBarLogoRedirect></NavBarLogoRedirect>
        <NavBarConditionalRedirect text="Priser" onClick={() => { navigate("/prices") }}></NavBarConditionalRedirect>
    <NavBarRedirect redirect="Frågor" href="#AccordionSection"></NavBarRedirect>
        <div className='Header-ButtonContainer'>
            <NavBarButtonRedirect onClick={() => { navigate("/login") }} text="Login"></NavBarButtonRedirect>
            <NavBarButtonRedirect onClick={() => { navigate("/register") }} text="Register"></NavBarButtonRedirect>
    </div>
    </>
    SmallHeaderContent = <>
    <NavBarLogoRedirect></NavBarLogoRedirect>
        <div className='Header-ButtonContainer'>
            <NavBarButtonRedirect onClick={() => { navigate("/login") }} text="Login"></NavBarButtonRedirect>
            <NavBarButtonRedirect onClick={() => { navigate("/register") }} text="Register"></NavBarButtonRedirect>
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

 return (<div className='Header-Container'><div className={GroupName}>{HeaderContent}</div></div>)
}

export default Header
