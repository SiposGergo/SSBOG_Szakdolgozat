import getMuiTheme from "material-ui/styles/getMuiTheme";
import { colors } from "./colors";

const customTheme = {
    palette: {
      primary1Color: colors["mid-dark-green"]
    },
    datePicker: {
      color: colors["darkest-green"],
      textColor: "#ffffff",
      calendarTextColor: "#000000",
      selectColor: colors["darkest-green"],
      selectTextColor: "#ffffff",
      calendarYearBackgroundColor: "#ffffff",
      headerColor: colors["darkest-green"]
    },
    timePicker: {
      color: colors["darkest-green"],
      textColor: "#ffffff",
      headerColor: colors["darkest-green"]
    },
    flatButton: {
      primaryTextColor: colors["darkest-green"]
    }
  };

  export const theme = getMuiTheme(customTheme);
