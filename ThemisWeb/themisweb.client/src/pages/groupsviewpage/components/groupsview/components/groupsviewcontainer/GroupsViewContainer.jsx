import React, { useEffect, useRef, useState } from 'react'
import "./GroupsViewContainer.css"
import icon from "./img_avatar.png"
import GroupView from './components/groupview/GroupView'
import useGroupsInfo from './hooks/useGroupsInfo'

const GroupsViewContainer = () => {
  useState
  const groupsInfo = useGroupsInfo();
  const [groupViewRows, setGroupViewRows] = useState([]);
  useEffect(() => {
    if(!groupsInfo){
      return;
    }
    let count = 0;
    let currRow = [];
    let temp = [];
    for(let i = 0; groupsInfo.length > i; i++){
      currRow.push({Name:groupsInfo[i].Name, Id:groupsInfo[i].Id, Img:groupsInfo[i].img})
      if(count == 3 || (i+1) == groupsInfo.length){
        temp.push(currRow);
        count = 0;
        currRow = [];
      }
    }
    setGroupViewRows(temp);
  }, [groupsInfo])
  return (
    <div className='GroupViewContainer'>
      {groupViewRows.map(i => <div className='GroupsViewContainer-Row'>{i.map(j => <GroupView img={j.Img} name={j.Name}></GroupView>)}</div>)}
    </div>
  )
}

export default GroupsViewContainer