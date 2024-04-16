import useFetch from "../hooks/useFetch";

export function getInstallerUrl(){
    return useFetch("/installer");
}