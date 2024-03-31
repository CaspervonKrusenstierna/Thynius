import React, { useContext } from 'react'
import "./GroupsViewHeader.css"
import { sessionInfoContext } from '../../../../../../App'
import { buttonVariants } from "@/components/ui/button"
import { Link } from 'react-router-dom';
import { ArrowRight } from '../../../../../../shared/assets';

const GroupsViewHeader = () => {
  const sessionInfo = useContext(sessionInfoContext);

  return (
    <>
    {(sessionInfo?.RoleLevel >= 1) ? 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p> 
        <Link to="/dashboard/creategroup/general" className={buttonVariants({ variant: "outline", size: "default"})}>
          <p className='text-base'>Skapa Grupp</p>
          <img src={ArrowRight} className='h-8 ml-4'></img>
        </Link>
      </div> : 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p>
      </div>
    }
   </>)
}

export default GroupsViewHeader