import React, { useContext } from 'react'
import "./GroupsViewHeader.css"
import { sessionInfoContext } from '../../../../../../App'
import { Link } from 'react-router-dom';
import {UserRoundPlus} from "lucide-react";
import { useTranslation } from 'react-i18next';

const GroupsViewHeader = () => {
  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  const [t, i18n] = useTranslation();

  return (
    <>
    {(sessionInfo?.RoleLevel >= 1) ? 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>{t("Groups")}</p> 
        <Link to="/dashboard/creategroup/general" className='flex flex-row gap-4 items-center h-10'>
          <p className="text-base font-bold">{t("Create Group")}</p>
          <UserRoundPlus size={30}></UserRoundPlus>
        </Link>
      </div> : 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>{t("Groups")}</p>
      </div>
    }
   </>)
}

export default GroupsViewHeader