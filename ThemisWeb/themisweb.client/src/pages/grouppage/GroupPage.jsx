import React from 'react'
import { useParams } from 'react-router-dom'

const GroupPage = () => {
  const { GroupId } = useParams();
  return (
    <div>GroupPage</div>
  )
}

export default GroupPage