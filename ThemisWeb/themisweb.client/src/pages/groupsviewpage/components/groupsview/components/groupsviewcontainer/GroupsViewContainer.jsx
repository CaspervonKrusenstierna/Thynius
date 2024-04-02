
import "./GroupsViewContainer.css"
import GroupView from './components/groupview/GroupView'
import useGroupsInfo from './hooks/useGroupsInfo'
import { useEffect, useState } from "react"
import useDynamicGroupsRowCount from "./hooks/useDynamicGroupsRowCount"
import { RowedItemsContainer } from "../../../../../../shared/components/dashboard"

const GroupsViewContainer = () => {
  const groupsPerRow = useDynamicGroupsRowCount(); // changes group per row count depending on the width of viewport
  const groupsInfo = useGroupsInfo();

  return (<div className="GroupsViewContainer">
      {<RowedItemsContainer ItemsPerRow={groupsPerRow} Items={groupsInfo?.map(j => <GroupView groupId={j.Id} managerId={j.ManagerId} img={j.Img} name={j.Name}></GroupView>)} Filler={<div className="GroupFiller"></div>}></RowedItemsContainer>}
      </div>
      )
}

export default GroupsViewContainer