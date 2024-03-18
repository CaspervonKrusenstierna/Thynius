import React, { useEffect, useState } from 'react'
import { NavBarButtonRedirect, NavBarLogoRedirect} from "./components"
import { useNavigate } from "react-router-dom";
import "./Header.css"

const Header = (props) => {
  const [GroupName, setGroupName] = useState("Header");
    const navigate = useNavigate();

    useEffect(() => {
        function OnScroll() {
            if (props.disableSticky) {
                return;
            }
            if (window.scrollY >= 110) {
                setGroupName("Header Sticky")
            }
            else {
                setGroupName("Header")
            }
        }
        document.onscroll = OnScroll;
    }
    , [])
        return (
        <div className='Header-Container'>
            <div className={GroupName}>
                <NavBarLogoRedirect></NavBarLogoRedirect>
                    <div className='Header-ButtonContainer'>
                        {props.loggedIn ? <NavBarButtonRedirect text="Dashboard" onClick={() => { navigate("/dashboard") }}></NavBarButtonRedirect> :
                            <>
                            <NavBarButtonRedirect onClick={() => { navigate("/login") }} text="Login"></NavBarButtonRedirect>
                            <NavBarButtonRedirect onClick={() => { navigate("/register") }} text="Register"></NavBarButtonRedirect>
                            </>
                        }
                    </div>
            </div>
        </div>
        )

}

export default Header
