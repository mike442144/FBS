        function MakeCorners( selector, size, imageFile, applyTo ) {
          $( selector ).css( "position", "relative" );
          if( applyTo != "bottom" ) {
            var classTl = imageFile + 'Tl';
            $( selector ).append( '<div class="' + classTl + '"></div>' );  
            $( '.' + classTl ).css( "position", "absolute" );
            $( '.' + classTl ).css( "top", "-3" );
            $( '.' + classTl ).css( "left", "-3" );
            $( '.' + classTl ).css( "width", size );
            $( '.' + classTl ).css( "height", size );
            $( '.' + classTl ).css( "background", "url(Content/Layout/" + imageFile + ".png) no-repeat top left" );
          }
          if( applyTo != "bottom" ) {
            var classTr = imageFile + 'Tr';
            $( selector ).append( '<div class="' + classTr + '"></div>' );          
            $( '.' + classTr ).css( "position", "absolute" );
            $( '.' + classTr ).css( "top", "-3" );
            $( '.' + classTr ).css( "right", "-3" );
            $( '.' + classTr ).css( "width", size );
            $( '.' + classTr ).css( "height", size );
            $( '.' + classTr ).css( "background", "url(Content/Layout/" + imageFile + ".png) no-repeat top right" );
          }
          if( applyTo != "top" ) {
            var classBl = imageFile + 'Bl';
            $( selector ).append( '<div class="' + classBl + '"></div>' );          
            $( '.' + classBl ).css( "position", "absolute" );
            $( '.' + classBl ).css( "bottom", "-3" );
            $( '.' + classBl ).css( "left", "-3" );
            $( '.' + classBl ).css( "width", size );
            $( '.' + classBl ).css( "height", size );
            $( '.' + classBl ).css( "background", "url(Content/Layout/" + imageFile + ".png) no-repeat bottom left" );
          }
          if( applyTo != "top" ) {
            var classBr = imageFile + 'Br';
            $( selector ).append( '<div class="' + classBr + '"></div>' );          
            $( '.' + classBr ).css( "position", "absolute" );
            $( '.' + classBr ).css( "bottom", "-3" );
            $( '.' + classBr ).css( "right", "-3" );
            $( '.' + classBr ).css( "width", size );
            $( '.' + classBr ).css( "height", size );
            $( '.' + classBr ).css( "background", "url(Content/Layout/" + imageFile + ".png) no-repeat bottom right" );
          }
        }
        
        function DoRoundedCorners() {
          MakeCorners( ".page", 24, "cornersPage", "all" );
          MakeCorners( "#title", 16, "cornersTitle", "all" );
          MakeCorners( "#menucontainer", 14, "cornersMain", "top" );
          MakeCorners( "#main", 14, "cornersMain", "bottom" );
          //MakeCorners( "h2", 12, "cornersHeader", "all" );
          MakeCorners( ".rightImage", 12, "cornersHeader", "all" );
        }