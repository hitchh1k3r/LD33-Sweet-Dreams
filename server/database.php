<?php

  $database = new SQLite3("shadows.sqlite");

  if($database)
  {
    $test = $database->query("SELECT name FROM sqlite_master WHERE type='table' AND name='shadows';");
    if($test)
    {
      $good = false;
      while ($row = $test->fetchArray())
      {
        if($row['name'] == "shadows")
        {
          $good = true;
          break;
        }
      }
      if(!$good)
      {
        $database->query("CREATE TABLE 'shadows' ('ID' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'name' TEXT NOT NULL, 'author' TEXT NOT NULL, 'data' TEXT NOT NULL);");
      }
    }
    else
    {
      $database = null;
    }
  }

?>