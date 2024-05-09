
import "./GroupsViewContainer.css"
import GroupView from './components/groupview/GroupView'
import useGroupsInfo from './hooks/useGroupsInfo'
import useDynamicGroupsRowCount from "./hooks/useDynamicGroupsRowCount"
import { RowedItemsContainer } from "../../../../../../shared/components/dashboard"

import { useRef } from "react"
import getGroupSkeletonItems from "./utils/getGroupSkeletonItems"

const GroupsViewContainer = () => {
  const groupsPerRow = useDynamicGroupsRowCount(); // changes group per row count depending on the width of viewport
  const [groupsInfo, setGroupsInfo] = useGroupsInfo();
  let isLoadedCount = useRef(0);

  return (
      <div className="GroupsViewContainer">
        { groupsInfo ?
          <RowedItemsContainer ItemsPerRow={groupsPerRow} Items={groupsInfo?.map(j => <GroupView groupsInfo={groupsInfo} setGroupsInfo={setGroupsInfo} key={j.Id} groupId={j.Id} managerId={j.ManagerId} img={j.PictureLink} name={j.Name} isLoadedCount={isLoadedCount}></GroupView>)} Filler={<div className="GroupFiller"></div>}></RowedItemsContainer>
          :
          <RowedItemsContainer ItemsPerRow={groupsPerRow} Items={getGroupSkeletonItems(groupsPerRow)}></RowedItemsContainer>
        }
      </div>
      )
}

export default GroupsViewContainer