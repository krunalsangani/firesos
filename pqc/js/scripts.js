$(document).ready(function () {
    // Smooth scrolling for navigation links
    $('a[href^="#"]').on("click", function (e) {
      e.preventDefault();
      const target = $($(this).attr("href"));
  
      if (target.length) {
        $("html, body").animate(
          {
            scrollTop: target.offset().top - 100,
          },
          1000,
          "easeInOutExpo",
        );
      }
    });
  
    // Navbar background opacity on scroll
    $(window).scroll(function () {
      if ($(this).scrollTop() > 100) {
        $(".navbar").css("background", "rgba(255, 255, 255, 0.2)");
      } else {
        $(".navbar").css("background", "rgba(255, 255, 255, 0.1)");
      }
    });
  
    // Fade in elements on scroll
    function fadeInOnScroll() {
      $(".fade-in").each(function () {
        const elementTop = $(this).offset().top;
        const elementBottom = elementTop + $(this).outerHeight();
        const viewportTop = $(window).scrollTop();
        const viewportBottom = viewportTop + $(window).height();
  
        if (elementBottom > viewportTop && elementTop < viewportBottom) {
          $(this).addClass("visible");
        }
      });
    }
  
    // Add fade-in class to elements
    $(".about-text, .service-card, .blog-post").addClass("fade-in");
  
    // Initial check for visible elements
    fadeInOnScroll();
  
    // Check for visible elements on scroll
    $(window).scroll($.throttle(250, fadeInOnScroll));
  
    // Button hover effects
    $(".btn").hover(
      function () {
        $(this).css("transform", "scale(1.05)");
      },
      function () {
        $(this).css("transform", "scale(1)");
      },
    );
  
    // Mobile menu toggle
    $(".navbar-toggler").click(function () {
      $(".navbar-collapse").slideToggle(300);
    });
  
    // Lazy loading for images
    const lazyLoadImages = function () {
      $("img[data-src]").each(function () {
        if (
          $(this).offset().top <
          $(window).height() + $(window).scrollTop() + 100
        ) {
          $(this).attr("src", $(this).data("src")).removeAttr("data-src");
        }
      });
    };
  
    // Initial lazy load check
    lazyLoadImages();
  
    // Check for lazy load on scroll
    $(window).scroll($.throttle(250, lazyLoadImages));
  
    // Form validation for contact forms
    $("form").submit(function (e) {
      e.preventDefault();
  
      const $form = $(this);
      const $submitButton = $form.find('button[type="submit"]');
  
      if ($form[0].checkValidity()) {
        $submitButton
          .prop("disabled", true)
          .html(
            '<span class="spinner-border spinner-border-sm"></span> Sending...',
          );
  
        // Simulate form submission
        setTimeout(function () {
          $submitButton
            .html("Sent!")
            .removeClass("btn-primary")
            .addClass("btn-success");
          setTimeout(function () {
            $submitButton
              .prop("disabled", false)
              .html("Send")
              .removeClass("btn-success")
              .addClass("btn-primary");
          }, 2000);
        }, 1500);
      } else {
        $form[0].reportValidity();
      }
    });
  
    // Add parallax effect to hero section
    $(window).scroll(function () {
      const scroll = $(window).scrollTop();
      $(".hero-section").css({
        "background-position": "center " + scroll * 0.5 + "px",
      });
    });
  
    // Initialize tooltips
    $('[data-bs-toggle="tooltip"]').tooltip();
  
    // Initialize popovers
    $('[data-bs-toggle="popover"]').popover();
  
    // Custom animation for section transitions
    function animateSections() {
      $(".animate-on-scroll").each(function () {
        const position = $(this).offset().top;
        const scroll = $(window).scrollTop();
        const windowHeight = $(window).height();
  
        if (scroll + windowHeight > position) {
          $(this).addClass("animated");
        }
      });
    }
  
    // Add animation classes to sections
    $(
      ".about-section, .services-grid, .showcase-section, .blog-section",
    ).addClass("animate-on-scroll");
  
    // Initial animation check
    animateSections();
  
    // Check for animations on scroll
    $(window).scroll($.throttle(250, animateSections));
  });
  document.addEventListener("DOMContentLoaded", () => {
    const counters = document.querySelectorAll(".counter-value");
    const duration = 2000;
    const steps = 60;
  
    function animateCounter(counter) {
      const target = parseInt(counter.getAttribute("data-target"));
      const increment = target / steps;
      let current = 0;
  
      const updateCounter = () => {
        current += increment;
        if (current < target) {
          counter.textContent = Math.round(current);
          requestAnimationFrame(updateCounter);
        } else {
          counter.textContent = target;
        }
      };
  
      updateCounter();
    }
  
    // Start animation when element is in viewport
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            animateCounter(entry.target);
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.5 },
    );
  
    counters.forEach((counter) => observer.observe(counter));
  });
  