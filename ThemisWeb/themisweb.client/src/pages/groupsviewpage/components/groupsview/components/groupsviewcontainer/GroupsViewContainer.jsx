
import "./GroupsViewContainer.css"
import GroupView from './components/groupview/GroupView'
import useGroupsInfo from './hooks/useGroupsInfo'
import RowedItemsContainer from '../../../../../../shared/components/roweditemscontainer/RowedItemsContainer'

const GroupsViewContainer = () => {
  const groupsInfo = useGroupsInfo();
  return (<div className="GroupsViewContainer">
      {<RowedItemsContainer ItemsPerRow={4} Items={groupsInfo?.map(j => <GroupView groupId={j.Id} img={j.Img} name={j.Name}></GroupView>)} Filler={<div className="Filler"></div>}></RowedItemsContainer>}
      </div>
      )
}

export default GroupsViewContainer