import React, { useEffect, useRef, useState } from 'react'
import "./GroupsViewContainer.css"
import GroupView from './components/groupview/GroupView'
import useGroupsInfo from './hooks/useGroupsInfo'

const GroupsViewContainer = () => {
  useState
  const groupsInfo = useGroupsInfo();
  const [groupViewRows, setGroupViewRows] = useState([[]]);
  useEffect(() => {
    if(!groupsInfo){
      return;
    }
    let count = 0;
    let currRow = [];
    let temp = [[]];
    for(let i = 0; groupsInfo.length > i; i++){
      currRow.push({Name:groupsInfo[i].Name, Id:groupsInfo[i].Id, Img:groupsInfo[i].img})

      if(count == 3 || (i+1) == groupsInfo.length){
        if(currRow.length == 2 && !(count == 3)){
          currRow.push("Filler")
          currRow.push("Filler")
        }
        else{
          for(let j = 0; (4-currRow.length) > j; j++){
            console.log(currRow.length);
            currRow.push("Filler")
          }
        }
        temp.push(currRow);
        count = 0;
        currRow = [];
        continue;
      }
      count++;
    }
    console.log(temp)
    setGroupViewRows(temp);
  }, [groupsInfo])
  return (
    <div className='GroupViewContainer'>
      {groupViewRows.map(i => {return <div className='GroupsViewContainer-Row'>{i.map(j => j=="Filler" ? <div className='Filler'></div> : <GroupView groupId={j.Id} img={j.Img} name={j.Name}></GroupView>)}</div>})}
    </div>
  )
}

export default GroupsViewContainer