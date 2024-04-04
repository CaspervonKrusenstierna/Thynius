import React, { useContext } from 'react'
import "./GroupsViewHeader.css"
import { sessionInfoContext } from '../../../../../../App'
import { buttonVariants } from "@/components/ui/button"
import { Link } from 'react-router-dom';
import { ArrowRight } from '../../../../../../shared/assets';
import {UserRoundPlus} from "lucide-react";

const GroupsViewHeader = () => {
  const sessionInfo = useContext(sessionInfoContext).sessionInfo;

  return (
    <>
    {(sessionInfo?.RoleLevel >= 1) ? 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p> 
        <Link to="/dashboard/creategroup/general" className='flex flex-row gap-4 items-center h-10'>
          <p className="text-base font-bold">Skapa Grupp</p>
          <UserRoundPlus size={30}></UserRoundPlus>
        </Link>
      </div> : 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p>
      </div>
    }
   </>)
}

export default GroupsViewHeader