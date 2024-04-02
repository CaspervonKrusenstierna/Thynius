import React, {useContext} from 'react'
import { useNavigate } from 'react-router-dom';
import "./DUCDropdown.css";
import { DUCLinkDropdownItem, DUCDownloadSoftwareDropdownItem, DUCEditProfileDropdownItem } from './components';
import useFetch from "../../../../../../../hooks/useFetch"
import { sessionInfoContext } from '../../../../../../../../App'
const DUCDropdown = (props) => {
  const updateSessionInfo = useContext(sessionInfoContext).updateSessionInfo;
  const navigate = useNavigate();

  async function onLogoutClick(){
      await useFetch("/account/logout", "POST");
      updateSessionInfo();
      navigate("/");
  }
  window.addEventListener("click", props.onClick);

  return (
    <div className='DUCDropdown animate-in fade-out-0 fade-in-0 zoom-out-95 zoom-in-95 slide-in-from-top-2'>
        {props.sizeState ? <DUCDownloadSoftwareDropdownItem></DUCDownloadSoftwareDropdownItem> : <></>} 
        <DUCEditProfileDropdownItem></DUCEditProfileDropdownItem>
        <DUCLinkDropdownItem onClick={onLogoutClick} link="" text="Logga ut"></DUCLinkDropdownItem>
    </div>
  )
}// no need to render the DUCDownloadSoftwareDropdownItem if we are on small screen because users can only download on computer anyways.

export default DUCDropdown