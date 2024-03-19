import React, { useContext } from 'react'
import "./GroupsViewHeader.css"
import { sessionInfoContext } from '../../../../../../App'
import CreateGroupButton from './creategroupbutton/CreateGroupButton';

const GroupsViewHeader = () => {
  const sessionInfo = useContext(sessionInfoContext);

  return (
    <>
    {(sessionInfo?.RoleLevel >= 1) ? 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p> <CreateGroupButton></CreateGroupButton>
      </div> : 
      <div className='GroupsViewHeader'>
        <p className='GroupsViewHeader-Title'>Grupper</p>
      </div>
    }
   </>)
}

export default GroupsViewHeader