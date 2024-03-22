import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import "../../shared/styles/Page.css"
import TextsViewPageView from './textsviewpageview/TextsViewPageView'

const TextsViewPage = () => {
  return (
    <div className='Page-Container'>
        <DashboardHeader></DashboardHeader>
        <TextsViewPageView></TextsViewPageView>
    </div>
  )
}

export default TextsViewPage