import React from 'react'
import "./DashboardUserComponentDropDown.css"
import UserComponentDropDownItem from './usercomponentdropdownitem/UserComponentDropDownItem'
import useFetch from '../../../../../../hooks/useFetch';
import { useNavigate } from 'react-router-dom';

const DashboardUserComponentDropDown = () => {
    const navigator = useNavigate();
    async function onLogoutClick(){
        await useFetch("/account/logout", "POST");
    }
  return (
    <div className='flex-column absolute mt-12 w-64'>
        <UserComponentDropDownItem link="/dashboard/profile" text="Profil"></UserComponentDropDownItem>
        <UserComponentDropDownItem onClick={onLogoutClick} link="/" text="Logga ut"></UserComponentDropDownItem>
    </div>
  )
}

export default DashboardUserComponentDropDown