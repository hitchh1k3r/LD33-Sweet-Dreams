<?php

  // PLEASE SEND SECURITY CONCERNS TO hitch@hitchh1k3rsguide.com
  require_once('database.php');
  if($database != null)
  {
    $query = $database->query("SELECT * FROM 'shadows' ORDER BY RANDOM() LIMIT 15;");
    if($query)
    {
      $build = '';
      while ($row = $query->fetchArray())
      {
        $build .= $row['name'] . '~' . $row['author'] . '~' . $row['data'] . '~';
      }
      echo $build;
    }
  }

?>