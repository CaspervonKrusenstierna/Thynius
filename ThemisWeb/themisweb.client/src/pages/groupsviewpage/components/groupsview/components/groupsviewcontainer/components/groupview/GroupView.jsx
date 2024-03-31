import React, { useContext } from 'react'
import "./GroupView.css"
import { Link } from 'react-router-dom'
import { sessionInfoContext } from '../../../../../../../../App'
import { ImageAvatar } from '../../../../../../../../shared/assets'
import GroupOptions from './groupoptions/GroupOptions'

const GroupView = (props) => {
  const sessionInfo = useContext(sessionInfoContext);
  return (
    <div className='flex flex-column rounded w-[320px] h-[275px] md:w-[275px] md:h-[250px] mt-[15px] shadow-light'>
      {sessionInfo?.ID == props.managerId ? <GroupOptions groupId={props.groupId}></GroupOptions> : <></>}
      
      <Link className='GroupView-Link' to={"/dashboard/group/" + props.groupId + "/Home"}>
        <img className="size-[185px] md:size-[150px] rounded mt-[45px] md:mt-[25px]" src={/*props.img*/ImageAvatar}></img>
        <p className="text-lg mt-2">{props.name}</p>
      </Link>
    </div>

  )
}

export default GroupView