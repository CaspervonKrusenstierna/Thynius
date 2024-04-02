import React, { useMemo, useState } from 'react'
import "./GroupMemberContainer.css"
import GroupMember from './groupmember/GroupMember'

const GroupMemberContainer = (props) => {


  return (
    <div className='GroupMemberContainer'>
          {props.members.map(s => <GroupMember User={s} onDelete={props.onMemberDelete}></GroupMember>)}
    </div>
  )
}

export default GroupMemberContainer