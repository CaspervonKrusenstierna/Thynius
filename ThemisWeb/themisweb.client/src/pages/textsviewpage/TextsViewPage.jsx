import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import TextsViewPageView from './textsviewpageview/TextsViewPageView'

const TextsViewPage = () => {
  return (
    <>
        <DashboardHeader></DashboardHeader>
        <div className="flex justify-center">
          <TextsViewPageView></TextsViewPageView>
        </div>
    </>
  )
}

export default TextsViewPage