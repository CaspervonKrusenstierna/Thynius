import React, {useContext} from 'react'
import { useNavigate } from 'react-router-dom';
import "./DUCDropdown.css";
import { DUCLinkDropdownItem, DUCDownloadSoftwareDropdownItem, DUCEditProfileDropdownItem } from './components';
import useFetch from "../../../../../../../hooks/useFetch"
import { sessionInfoContext } from '../../../../../../../../App'
import { useTranslation } from 'react-i18next';
const DUCDropdown = (props) => {
  const updateSessionInfo = useContext(sessionInfoContext).updateSessionInfo;
  const navigate = useNavigate();
  const [t, i18n] = useTranslation();

  async function onLogoutClick(){
      await useFetch("/logout", "POST");
      updateSessionInfo();
      navigate("/");
  }
  window.addEventListener("click", props.onClick);

  return (
    <div className='DUCDropdown animate-in fade-out-0 fade-in-0 zoom-out-95 zoom-in-95 slide-in-from-top-2'>
        {props.sizeState ? <DUCDownloadSoftwareDropdownItem></DUCDownloadSoftwareDropdownItem> : <></>} 
        <DUCEditProfileDropdownItem></DUCEditProfileDropdownItem>
        <DUCLinkDropdownItem onClick={onLogoutClick} link="" text={t("Logout")}></DUCLinkDropdownItem>
    </div>
  )
}// no need to render the DUCDownloadSoftwareDropdownItem if we are on small screen because users can only download on computer anyways.

export default DUCDropdown