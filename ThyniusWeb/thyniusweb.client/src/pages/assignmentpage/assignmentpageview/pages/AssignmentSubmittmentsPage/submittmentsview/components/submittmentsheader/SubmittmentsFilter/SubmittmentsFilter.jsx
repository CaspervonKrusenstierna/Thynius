import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
  } from "@/components/ui/select"
  import React from 'react'
import { useTranslation } from "react-i18next"
  
  const SubmittmentsFilter = (props) => {
    const [t, i18n] = useTranslation();
    return (
        <Select onValueChange={(i) => {props.setFilter(i)}}>
            <SelectTrigger className="w-[180px]">
            <SelectValue placeholder={t("Filter")} />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value={0}>{t("View all")}</SelectItem>
              <SelectItem value={1}>{t("No signs of cheating")}</SelectItem>
              <SelectItem value={2}>{t("Signs of cheating")}</SelectItem>
              <SelectItem value={3}>{t("Serious signs of cheating")}</SelectItem>
            </SelectContent>
        </Select>
    )
  }
  
  export default SubmittmentsFilter