
import { format } from "date-fns"
import { Calendar as CalendarIcon } from "lucide-react"
 
import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import { Calendar } from "@/components/ui/calendar"
import { sv, enUS } from "date-fns/locale"

import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { useTranslation } from "react-i18next"

 
export function DatePicker(props) {
  const [t, i18n] = useTranslation();
  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button
          variant={"outline"}
          className={cn(
            "w-full sm:w-[280px] justify-start text-left font-normal",
            !props.date && "text-muted-foreground"
          )}
        >
          <CalendarIcon className="mr-2 h-4 w-4" />
          {props.date ? format(props.date, "PPP", {locale: i18n.language == "sv-SE" ? sv : enUS}) : <span>{t("Deadline")}</span>}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-auto p-0">
        <Calendar
          locale={i18n.language == "sv-SE" ? sv : enUS}
          mode="single"
          selected={props.date}
          onSelect={props.setDate}
          initialFocus
        />
      </PopoverContent>
    </Popover>
  )
}