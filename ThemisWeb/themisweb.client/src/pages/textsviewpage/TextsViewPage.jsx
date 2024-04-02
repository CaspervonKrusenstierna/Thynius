import React from 'react'
import TextsViewPageView from './textsviewpageview/TextsViewPageView'
import { DashboardHeader } from "../../shared/components/dashboard"

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